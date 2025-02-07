# PeltierController
-------------------------------------------
Author: Bram Laurens
Title: Peltier Controller
Date: 2025


This is a simple self-contained Windows application to control a peltier module connected to a Roboteq
VRS1460 motor controller.

Instructions:
1. Connect controller to USB.
2. Power on the driver

BEFORE CONTINUING: Double check that the fan is properly connected to the power supply on the Roboteq driver. Also confirm that the fan is spinning! 
Powering the peltier module without the fan spinning can result in heat damage!

3. Open the program
4. Select the correct COM port from the drop down menu at the bottom, you might need to refresh the list.
5. Select cooling or heating mode
6. Enter a PWM value between 0 and 100 for cooling and 0 and 40 for heating in the text field, next to the PWM button. The unit is duty cycle %.
7. CLick "Set PWM Duty cycle"
8. Your module should now heat or cool!
9. Click reset to turn module off (set PWM duty cycle to 0)

Note: it takes 10 minutes for the module to stabilize temperature once given a PWM duty cycle

No additional programs required.

---------------------------------------------
# Upcoming features:
Temperature control
