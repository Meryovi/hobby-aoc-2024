package problems

import (
	"image"
	"strings"

	"github.com/Meryovi/hobby-aoc-2024/go/shared"
)

type Day4 struct{}

func (d Day4) Solve(input string) int {
	r := 0

	matrix := strings.Split(input, shared.NewLine)
	my := len(matrix)
	mx := len(matrix[0])

	for x := range mx {
		for y := range my {
			p := image.Point{x, y}
			r += countWordMatches(matrix, "XMAS", p)
		}
	}

	return r
}

func countWordMatches(matrix []string, word string, pos image.Point) int {
	dirs := []image.Point{{1, 0}, {-1, 0}, {0, 1}, {0, -1}, {1, 1}, {1, -1}, {-1, 1}, {-1, -1}}
	matches := 0

	for _, dir := range dirs {
		p := pos
		for i := range word {
			if p.Y >= len(matrix) || p.X >= len(matrix[0]) || p.Y < 0 || p.X < 0 {
				break
			}

			if word[i] != matrix[p.Y][p.X] {
				break
			}

			if i == len(word)-1 {
				matches++
				break
			}

			p = p.Add(dir)
		}
	}

	return matches
}
