import asyncio

import uiautomator2 as u2

import cmd
import config
class Device:
    name : str
    index : int
    device : u2.Device
    def __init__(self,name,index,device):
        self.name = name
        self.index = index
        self.device = device
    def __str__(self):
        return f"name: {self.name}, index: {self.index}"


devices = []
def get_device(i) -> Device:
    for device in devices:
        if device.index == i:
            return device
async def get_devices():
    devices.clear()
    dv = cmd.get_list_adb_devices()
    while len(dv) != config.ld_tab:
        dv = cmd.get_list_adb_devices()
    for i in range(len(dv)):
        device = Device(dv[i],i+1,u2.connect(dv[i]))
        devices.append(device)
asyncio.run(get_devices())
