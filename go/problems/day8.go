package problems

import (
	"image"
	"strings"

	"github.com/Meryovi/hobby-aoc-2024/go/shared"
)

type Day8 struct{}

func (d Day8) Solve(input string) int {
	matrix := strings.Split((input), shared.NewLine)
	my := len(matrix)
	mx := len(matrix[0])

	groups := make(map[byte][]image.Point)
	antiNodes := make(map[image.Point]image.Point)

	for y := range my {
		for x := range mx {
			c := matrix[y][x]
			if c != '.' {
				pt := image.Pt(x, y)
				groups[c] = append(groups[c], pt)
			}
		}
	}

	for _, antennas := range groups {
		for i := range len(antennas) - 1 {
			for j := i + 1; j < len(antennas); j++ {
				a := antennas[i]
				b := antennas[j]
				d := a.Sub(b)

				antiA := image.Pt(a.X+d.X, a.Y+d.Y)
				antiB := image.Pt(b.X-d.X, b.Y-d.Y)

				if inBounds(antiA, mx, my) {
					antiNodes[antiA] = antiA
				}
				if inBounds(antiB, mx, my) {
					antiNodes[antiB] = antiB
				}
			}
		}
	}

	return len(antiNodes)
}

func inBounds(pt image.Point, mx, my int) bool {
	return pt.X >= 0 && pt.X < mx && pt.Y >= 0 && pt.Y < my
}
