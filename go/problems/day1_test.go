package problems

import (
	"testing"
)

func TestDay1_TestSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day1{}, "day1_1", 11)
}

func TestDay1_FullSet_YieldsExpectedResult(t *testing.T) {
	TestAocProblem(t, Day1{}, "day1_2", 1110981)
}
