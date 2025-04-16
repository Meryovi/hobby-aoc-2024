package problems

import "testing"

func TestDay10_TestSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day10{}, "day10_1", 36)
}

func TestDay10_FullSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day10{}, "day10_2", 822)
}
