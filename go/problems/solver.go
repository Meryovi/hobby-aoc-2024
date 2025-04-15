package problems

type Solver[T any] interface {
	Solve(input string) T
}
