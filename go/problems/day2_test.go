package problems

import "testing"

func TestDay2_TestSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day2{}, "day2_1", 2)
}

func TestDay2_FullSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day2{}, "day2_2", 486)
}
