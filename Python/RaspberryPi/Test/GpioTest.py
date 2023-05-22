import time
import RPi.GPIO as GPIO

# print(str(GPIO.VERSION))

GPIO.setmode(GPIO.BOARD)
GPIO.setwarnings(False)

GPIO.setup(12, GPIO.OUT)

for i in range(1,100):
    time.sleep(1)
    GPIO.output(12, i%2)
    print(str(GPIO.input(12)))

# 该句会重置为出厂
# GPIO.cleanup()
