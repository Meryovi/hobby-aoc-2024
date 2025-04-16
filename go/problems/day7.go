package problems

import (
	"strconv"
	"strings"

	"github.com/Meryovi/hobby-aoc-2024/go/shared"
)

type Day7 struct{}

func (d Day7) Solve(input string) int64 {
	var r int64 = 0
	nms := make([]int64, 0)

	for _, line := range strings.Split(input, shared.NewLine) {
		nms = nms[:0] // reset the slice...
		split := strings.Split(line, ":")

		for _, sn := range strings.Split(split[1], " ") {
			n, _ := strconv.ParseInt(sn, 10, 64)
			nms = append(nms, n)
		}

		target, _ := strconv.ParseInt(split[0], 10, 64)
		if operatorEqualsValue(target, nms[0], nms, 1) {
			r += target
		}
	}

	return r
}

func operatorEqualsValue(target, curr int64, nms []int64, inx int) bool {
	if curr > target {
		return false
	}

	// compare only in the last iteration
	if inx == len(nms) {
		return curr == target
	}

	add := curr + nms[inx]
	prod := curr * nms[inx]

	return operatorEqualsValue(target, add, nms, inx+1) || operatorEqualsValue(target, prod, nms, inx+1)
}
