package problems

import (
	"testing"

	"github.com/Meryovi/hobby-aoc-2024/go/shared"
)

func AssertEqual[T comparable](t *testing.T, want, got T) {
	t.Helper()
	if want != got {
		t.Errorf("expected %v, got %v", want, got)
	}
}

func TestAocProblem[T comparable](t *testing.T, solver Solver[T], problem string, want T) {
	input, err := shared.ReadProblemInput(problem)
	if err != nil {
		t.Fatal("could not read input file")
	}
	res := solver.Solve(input)
	AssertEqual(t, want, res)
}
