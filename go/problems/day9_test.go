package problems

import "testing"

func TestDay9_TestSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day9{}, "day9_1", 1928)
}

func TestDay9_FullSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day9{}, "day9_2", 6242766523059)
}
