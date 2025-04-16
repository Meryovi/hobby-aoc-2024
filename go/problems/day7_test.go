package problems

import "testing"

func TestDay7_TestSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day7{}, "day7_1", 3749)
}

func TestDay7_FullSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day7{}, "day7_2", 1298103531759)
}
