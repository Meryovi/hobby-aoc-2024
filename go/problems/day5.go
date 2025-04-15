package problems

import (
	"slices"
	"strconv"
	"strings"

	"github.com/Meryovi/hobby-aoc-2024/go/shared"
)

type Day5 struct{}

func (d Day5) Solve(input string) int {
	split := strings.Split(input, shared.NewLine+shared.NewLine)
	ordering := strings.Split(split[0], shared.NewLine)
	updates := strings.Split(split[1], shared.NewLine)

	rules := make(map[int][]int)

	for _, o := range ordering {
		split := strings.Split(o, "|")
		greater, _ := strconv.Atoi(split[0])
		lower, _ := strconv.Atoi(split[1])

		if _, exists := rules[greater]; !exists {
			rules[greater] = []int{lower}
		} else {
			rules[greater] = append(rules[greater], lower)
		}
	}

	r := 0

	for _, u := range updates {
		split := strings.Split(u, ",")
		pages := make([]int, len(split))

		for i, sp := range split {
			pages[i], _ = strconv.Atoi(sp)
		}

		if pagesOrdered(pages, rules) {
			r += pages[len(pages)/2]
		}
	}

	return r
}

func pagesOrdered(pages []int, rules map[int][]int) bool {
	for i := range len(pages) - 1 {
		cp := pages[i]
		r, exists := rules[cp]
		if !exists {
			return false
		}

		for j := i + 1; j < len(pages); j++ {
			np := pages[j]
			if !slices.Contains(r, np) {
				return false
			}
		}
	}
	return true
}
