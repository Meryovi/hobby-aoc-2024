package problems

import "testing"

func TestDay6_TestSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day6{}, "day6_1", 41)
}

func TestDay6_FullSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day6{}, "day6_2", 4973)
}
