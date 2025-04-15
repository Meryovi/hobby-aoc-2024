package problems

import "testing"

func TestDay4_TestSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day4{}, "day4_1", 18)
}

func TestDay4_FullSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day4{}, "day4_2", 2575)
}
