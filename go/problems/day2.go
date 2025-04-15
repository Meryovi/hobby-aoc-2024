package problems

import (
	"strconv"
	"strings"

	"github.com/Meryovi/hobby-aoc-2024/go/shared"
)

type Day2 struct{}

func (d Day2) Solve(input string) int {
	r := 0

	for _, line := range strings.Split(input, shared.NewLine) {
		values := strings.Split(line, " ")
		levels := make([]int, len(values))

		for i, v := range values {
			levels[i], _ = strconv.Atoi(v)
		}

		inc := levels[0] < levels[1]
		r++

		for i := 1; i < len(levels); i++ {
			prev := levels[i-1]
			curr := levels[i]
			diff := 0

			if inc {
				diff = curr - prev
			} else {
				diff = prev - curr
			}

			if diff < 1 || diff > 3 {
				r--
				break
			}
		}
	}

	return r
}
