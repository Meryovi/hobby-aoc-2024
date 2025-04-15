package problems

import (
	"slices"
	"strconv"
	"strings"

	"github.com/Meryovi/hobby-aoc-2024/go/shared"
)

type Day1 struct{}

func (d Day1) Solve(input string) int {
	lines := strings.Split(input, shared.NewLine)
	right := make([]int, len(lines))
	left := make([]int, len(lines))

	for i, line := range strings.Split(input, shared.NewLine) {
		split := strings.Split(line, "   ")
		left[i], _ = strconv.Atoi(split[0])
		right[i], _ = strconv.Atoi(split[1])
	}

	slices.Sort(left)
	slices.Sort(right)

	r := 0

	for i := range left {
		r += IntAbs(left[i] - right[i])
	}

	return r
}

func IntAbs(n int) int {
	if n < 0 {
		return -n
	}
	return n
}
