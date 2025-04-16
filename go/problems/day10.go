package problems

import (
	"image"
	"math"
	"strings"

	"github.com/Meryovi/hobby-aoc-2024/go/shared"
)

type Day10 struct{}

type pointPair struct {
	start image.Point
	end   image.Point
}

func (d Day10) Solve(input string) int {
	paths := make(map[pointPair]pointPair)
	matrix := strings.Split(input, shared.NewLine)

	for y := range matrix {
		for x := range matrix[0] {
			start := image.Pt(x, y)
			buildTrailPaths(matrix, start, start, paths, 0)
		}
	}

	return len(paths)
}

var navDirs = []image.Point{right, down, left, up}

func buildTrailPaths(matrix []string, start, curr image.Point, paths map[pointPair]pointPair, expected int) {
	val := math.MinInt
	if curr.Y >= 0 && curr.X >= 0 && curr.Y < len(matrix) && curr.X < len(matrix[0]) {
		val = int(matrix[curr.Y][curr.X] - '0')
	}

	if val != expected {
		return
	}

	if expected == 9 {
		pp := pointPair{start, curr}
		paths[pp] = pp
		return
	}

	for _, dir := range navDirs {
		buildTrailPaths(matrix, start, curr.Add(dir), paths, expected+1)
	}
}
