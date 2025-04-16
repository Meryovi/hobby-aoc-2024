package problems

type Day9 struct{}

var emptySpace int = -1

func (d Day9) Solve(input string) int64 {
	universe := make([]int, 0, len(input))

	for i := 0; i < len(input); i += 2 {
		fileId := i / 2
		files := int(input[i] - '0')
		spaces := 0
		if i < len(input)-1 {
			spaces = int(input[i+1] - '0')
		}

		ul := len(universe)
		universe = append(universe, make([]int, files+spaces)...)

		for i := range files {
			universe[ul+i] = fileId
		}
		for i := range spaces {
			universe[ul+files+i] = emptySpace
		}
	}

	var r int64 = 0

	for head, tail := 0, len(universe); head < tail; head++ {
		val := universe[head]

		if val == emptySpace {
			tail--
			for tail > head {
				val = universe[tail]
				if val != emptySpace {
					break
				}
				tail--
			}
		}

		r += int64(val * head)
	}

	return r
}
