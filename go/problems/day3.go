package problems

import (
	"regexp"
	"strconv"
)

type Day3 struct{}

func (d Day3) Solve(input string) int {
	r := 0

	rx := regexp.MustCompile(`mul\((\d{1,3}),(\d{1,3})\)`)
	matches := rx.FindAllStringSubmatch(input, -1)

	for _, m := range matches {
		left, _ := strconv.Atoi(m[1])
		right, _ := strconv.Atoi(m[2])
		r += left * right
	}

	return r
}
