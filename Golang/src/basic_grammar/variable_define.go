package basic_grammar

import "fmt"

func VariableDefine() {
	/*
		这三种定义变量方式结果是相同的
	*/
	var str1 string = "Runoob"
	var str2 = "Runoob"
	str3 := "Runoob"
	fmt.Println(str1, str2, str3)

	/*
		定义多个变量
	*/
	var multi1, multi2 int = 1, 2
	var multi11, multi22 = 1, 2
	fmt.Println(multi1, multi2, multi11, multi22)

	/*
		定义变量，但不给初始值
	*/
	var i int
	var f float64
	var b2 bool
	var s string
	fmt.Printf("%v %v %v %q\n", i, f, b2, s)

	/*
		显式类型定义： const b string = "abc"
		隐式类型定义： const b = "abc"
	*/
	const const1 string = "abc"
	fmt.Println(const1)

	/*
		常量枚举
	*/
	const (
		Unknown = 0
		Female  = 1
		Male    = 2
	)
	fmt.Println(Unknown, Female, Male)
}
