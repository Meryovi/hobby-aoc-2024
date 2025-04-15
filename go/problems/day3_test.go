package problems

import "testing"

func TestDay3_TestSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day3{}, "day3_1", 161)
}

func TestDay3_FullSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day3{}, "day3_2", 188741603)
}
