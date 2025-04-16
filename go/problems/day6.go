package problems

import (
	"image"
	"strings"

	"github.com/Meryovi/hobby-aoc-2024/go/shared"
)

type Day6 struct{}

var (
	up    = image.Pt(0, -1)
	down  = image.Pt(0, 1)
	right = image.Pt(1, 0)
	left  = image.Pt(-1, 0)
)

func (d Day6) Solve(input string) int {
	h := make(map[image.Point]image.Point)
	matrix := strings.Split(input, shared.NewLine)

	pt := seekStartPoint(matrix, '^')
	dir := up
	h[pt] = pt

	for {
		next := pt.Add(dir)
		if next.Y >= len(matrix) || next.X >= len(matrix[0]) || next.Y < 0 || next.X < 0 {
			break
		}

		c := matrix[next.Y][next.X]
		if c == '#' {
			dir = turnRight(dir)
			continue
		}

		pt = next
		h[pt] = pt
	}

	return len(h)
}

func seekStartPoint(matrix []string, char rune) image.Point {
	for y, l := range matrix {
		for x, c := range l {
			if c == char {
				return image.Pt(x, y)
			}
		}
	}
	return image.Pt(0, 0)
}

func turnRight(dir image.Point) image.Point {
	switch dir {
	case up:
		return right
	case down:
		return left
	case right:
		return down
	case left:
		return up
	default:
		return dir
	}
}
