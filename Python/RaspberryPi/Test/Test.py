# coding=utf-8
import time
import datetime
import sys
sys.path.append('l..')
reload(sys)
sys.setdefaultencoding('utf8')

# import RPi.GPIO as GPIO

# print(str(GPIO.VERSION))

# GPIO.setmode(GPIO.BOARD)
# GPIO.setwarnings(False)

# GPIO.setup(12, GPIO.OUT)

# for i in range(1,100):
#     time.sleep(1)
#     GPIO.output(12, i%2)
#     print(str(GPIO.input(12)))

# GPIO.cleanup()

timenow = datetime.datetime.now()
print(timenow.date().year)
print(str(timenow.date().month).zfill(2))
print(str(timenow.date().day).zfill(2))
print(str(timenow.time().hour).zfill(2))
print(str(timenow.time().minute).zfill(2))
