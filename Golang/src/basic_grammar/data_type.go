package basic_grammar

import "fmt"

func DataType() {
	/*
		布尔型:
		布尔型的值只可以是常量 true 或者 false。一个简单的例子：var b bool = true。
	*/
	boolvariable := false
	fmt.Printf("this is a boolean variable: %v", boolvariable)
	fmt.Println()

	/*
		数字类型:
		整型 int 和浮点型 float32、float64，Go 语言支持整型和浮点型数字，并且支持复数，其中位的运算采用补码。
	*/
	intvariable := 132123
	fmt.Printf("this is a int variable: %v", intvariable)
	fmt.Println()

	/*
		字符串类型:
		字符串就是一串固定长度的字符连接起来的字符序列。Go 的字符串是由单个字节连接起来的。Go 语言的字符串的字节使用 UTF-8 编码标识 Unicode 文本。
	*/
	strvariable := "this is a str"
	fmt.Println(strvariable)

	/*
		派生类型:
		包括：
		(a) 指针类型（Pointer）
		(b) 数组类型
		(c) 结构化类型(struct)
		(d) Channel 类型
		(e) 函数类型
		(f) 切片类型
		(g) 接口类型（interface）
		(h) Map 类型
	*/
}
