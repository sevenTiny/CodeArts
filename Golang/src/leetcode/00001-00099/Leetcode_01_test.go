/*
给定一个整数数组 nums 和一个整数目标值 target，请你在该数组中找出 和为目标值 target  的那 两个 整数，并返回它们的数组下标。

你可以假设每种输入只会对应一个答案。但是，数组中同一个元素在答案里不能重复出现。

你可以按任意顺序返回答案。



示例 1：

输入：nums = [2,7,11,15], target = 9
输出：[0,1]
解释：因为 nums[0] + nums[1] == 9 ，返回 [0, 1] 。
示例 2：

输入：nums = [3,2,4], target = 6
输出：[1,2]
示例 3：

输入：nums = [3,3], target = 6
输出：[0,1]
*/

package leetcode

import (
	"github.com/stretchr/testify/assert"
	"testing"
)

func Test_Leetcode_01_twoSum(t *testing.T) {
	nums := []int{2, 7, 11, 15}
	target := 9

	re := twoSum(nums, target)

	a := assert.New(t)
	a.ElementsMatch([]int{0, 1}, re)
}

func twoSum(nums []int, target int) []int {
	dic := make(map[int]int)
	for i, v := range nums {
		index, ok := dic[target-v]
		if ok {
			return []int{i, index}
		}
		dic[v] = i
	}

	return []int{}
}

func Test_Leetcode_01_twoSum_1(t *testing.T) {
	nums := []int{2, 7, 11, 15}
	target := 9

	re := twoSum_1(nums, target)

	a := assert.New(t)
	a.ElementsMatch([]int{0, 1}, re)
}

func twoSum_1(nums []int, target int) []int {
	var re []int
	for i := 0; i < len(nums); i++ {
		j := 0
		for j < i {
			if nums[i]+nums[j] == target {
				re = append(re, j)
				re = append(re, i)
			}
			j++
		}
	}

	return re
}
