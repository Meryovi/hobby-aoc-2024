package problems

import "testing"

func TestDay5_TestSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day5{}, "day5_1", 143)
}

func TestDay5_FullSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day5{}, "day5_2", 7307)
}
