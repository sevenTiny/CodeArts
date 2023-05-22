# coding=utf-8
import sys
sys.path.append('..')

reload(sys)
sys.setdefaultencoding('utf8')

import time
import RPi.GPIO as GPIO

# 共阳4位数字管
class Yang4():
    # 显示位数
    p1 = 1
    p2 = 2
    p3 = 3
    p4 = 4
    # 显示状态
    a = 5
    b = 6
    c = 7
    d = 8
    e = 9
    f = 10
    g = 11
    dp = 12
    positionPoints = []
    numberPoints = []

    # 初始化并设置控制针脚
    # 针脚连接顺序：位置1-4，数字a-dp
    def __init__(self, p1, p2, p3, p4, a, b, c, d, e, f, g, dp):
        self.p1 = p1
        self.p2 = p2
        self.p3 = p3
        self.p4 = p4
        self.a = a
        self.b = b
        self.c = c
        self.d = d
        self.e = e
        self.f = f
        self.g = g
        self.dp = dp
        self.positionPoints = [p1, p2, p3, p4]
        self.numberPoints = [a, b, c, d, e, f, g, dp]

        # Board模式
        GPIO.setmode(GPIO.BOARD)
        # 关闭提示
        GPIO.setwarnings(False)

        for item in self.positionPoints+self.numberPoints:
            GPIO.setup(item, GPIO.OUT)

    # 输入一个字符串
    def Display(self, str8bit):
        self.__DisplayCode(str8bit)

    # 筛选并控制显示各位置
    def __DisplayCode(self, str8bit):
        # 当前位置
        index = -1
        for i in range(0, len(str8bit)):
            if index > 8:
                return

            arg = str(str8bit[i])
            if arg == '.' and index % 2 != 0:
                index = index + 1
            elif arg != '.' and index % 2 != 1:
                index = index + 1
            
            index = index + 1

            self.__ResetPosition()
            self.__ResetNumber()
            self.__DisplayNumberSwitch(arg)
            GPIO.output(self.positionPoints[index//2], 1)
            time.sleep(0.002)


    def __ResetPosition(self):
        for item in self.positionPoints:
            GPIO.output(item, 0)

    def __ResetNumber(self):
        for item in self.numberPoints:
            GPIO.output(item, 1)

    def __DisplayNumberSwitch(self, arg):
        # print('arg='+str(arg))
        if arg == '.':
            self.__Display_DOT()
        # 上方小圈用小o，下方小圈用中文句号
        elif arg == 'o':
            self.__Display_TopCircle()
        elif arg == '。':
            self.__Display_DownCircle()
        # -----------------------------
        elif arg == '0':
            self.__Display_0()
        elif arg == '1':
            self.__Display_1()
        elif arg == '2':
            self.__Display_2()
        elif arg == '3':
            self.__Display_3()
        elif arg == '4':
            self.__Display_4()
        elif arg == '5':
            self.__Display_5()
        elif arg == '6':
            self.__Display_6()
        elif arg == '7':
            self.__Display_7()
        elif arg == '8':
            self.__Display_8()
        elif arg == '9':
            self.__Display_9()
        # -----------------------------
        elif arg == 'A':
            self.__Display_A()
        elif arg == 'B':
            self.__Display_B()
        elif arg == 'C':
            self.__Display_C()
        elif arg == 'D':
            self.__Display_D()
        elif arg == 'd':
            self.__Display_d()
        elif arg == 'E':
            self.__Display_E()
        elif arg == 'F':
            self.__Display_F()
        elif arg == 'G':
            self.__Display_G()
        elif arg == 'H':
            self.__Display_H()
        elif arg == 'I':
            self.__Display_I()
        elif arg == 'J':
            self.__Display_J()
        elif arg == 'L':
            self.__Display_L()
        elif arg == 'O':
            self.__Display_O()
        elif arg == 'P':
            self.__Display_P()
        elif arg == 'S':
            self.__Display_S()
        elif arg == 'U':
            self.__Display_U()
        elif arg == 'V':
            self.__Display_V()
        else:
            None

    def __Display_DOT(self):
        GPIO.output(self.dp, 0)

    def __Display_TopCircle(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.b, 0)
        GPIO.output(self.g, 0)
        GPIO.output(self.f, 0)

    def __Display_DownCircle(self):
        GPIO.output(self.c, 0)
        GPIO.output(self.d, 0)
        GPIO.output(self.e, 0)
        GPIO.output(self.g, 0)

    # -----------------------------
    def __Display_0(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.b, 0)
        GPIO.output(self.c, 0)
        GPIO.output(self.d, 0)
        GPIO.output(self.e, 0)
        GPIO.output(self.f, 0)

    def __Display_1(self):
        GPIO.output(self.b, 0)
        GPIO.output(self.c, 0)

    def __Display_2(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.b, 0)
        GPIO.output(self.d, 0)
        GPIO.output(self.e, 0)
        GPIO.output(self.g, 0)

    def __Display_3(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.b, 0)
        GPIO.output(self.c, 0)
        GPIO.output(self.d, 0)
        GPIO.output(self.g, 0)

    def __Display_4(self):
        GPIO.output(self.b, 0)
        GPIO.output(self.c, 0)
        GPIO.output(self.f, 0)
        GPIO.output(self.g, 0)

    def __Display_5(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.c, 0)
        GPIO.output(self.d, 0)
        GPIO.output(self.f, 0)
        GPIO.output(self.g, 0)

    def __Display_6(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.c, 0)
        GPIO.output(self.d, 0)
        GPIO.output(self.e, 0)
        GPIO.output(self.f, 0)
        GPIO.output(self.g, 0)

    def __Display_7(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.b, 0)
        GPIO.output(self.c, 0)

    def __Display_8(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.b, 0)
        GPIO.output(self.c, 0)
        GPIO.output(self.d, 0)
        GPIO.output(self.e, 0)
        GPIO.output(self.f, 0)
        GPIO.output(self.g, 0)

    def __Display_9(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.b, 0)
        GPIO.output(self.c, 0)
        GPIO.output(self.d, 0)
        GPIO.output(self.f, 0)
        GPIO.output(self.g, 0)

    # -----------------------------
    def __Display_A(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.b, 0)
        GPIO.output(self.c, 0)
        GPIO.output(self.e, 0)
        GPIO.output(self.f, 0)
        GPIO.output(self.g, 0)

    def __Display_B(self):
        self.__Display_8()

    def __Display_C(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.d, 0)
        GPIO.output(self.e, 0)
        GPIO.output(self.f, 0)

    def __Display_d(self):
        GPIO.output(self.b, 0)
        GPIO.output(self.c, 0)
        GPIO.output(self.d, 0)
        GPIO.output(self.e, 0)
        GPIO.output(self.g, 0)

    def __Display_D(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.b, 0)
        GPIO.output(self.c, 0)
        GPIO.output(self.d, 0)
        GPIO.output(self.e, 0)
        GPIO.output(self.f, 0)

    def __Display_E(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.d, 0)
        GPIO.output(self.e, 0)
        GPIO.output(self.f, 0)
        GPIO.output(self.g, 0)

    def __Display_F(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.e, 0)
        GPIO.output(self.f, 0)
        GPIO.output(self.g, 0)

    def __Display_G(self):
        self.__Display_6()

    def __Display_H(self):
        GPIO.output(self.b, 0)
        GPIO.output(self.c, 0)
        GPIO.output(self.e, 0)
        GPIO.output(self.f, 0)
        GPIO.output(self.g, 0)

    def __Display_I(self):
        self.__Display_1()

    def __Display_J(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.b, 0)
        GPIO.output(self.c, 0)
        GPIO.output(self.d, 0)

    def __Display_L(self):
        GPIO.output(self.d, 0)
        GPIO.output(self.e, 0)
        GPIO.output(self.f, 0)

    def __Display_O(self):
        self.__Display_0()

    def __Display_P(self):
        GPIO.output(self.a, 0)
        GPIO.output(self.b, 0)
        GPIO.output(self.e, 0)
        GPIO.output(self.f, 0)
        GPIO.output(self.g, 0)

    def __Display_S(self):
        self.__Display_5()

    def __Display_U(self):
        GPIO.output(self.b, 0)
        GPIO.output(self.c, 0)
        GPIO.output(self.d, 0)
        GPIO.output(self.e, 0)
        GPIO.output(self.f, 0)

    def __Display_V(self):
        self.__Display_U()
