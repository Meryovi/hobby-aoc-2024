package problems

import "testing"

func TestDay8_TestSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day8{}, "day8_1", 14)
}

func TestDay8_FullSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day8{}, "day8_2", 228)
}
