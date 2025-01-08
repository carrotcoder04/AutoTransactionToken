import subprocess

import config


def get_command(command):
    try:
        result = subprocess.run(command, stdout=subprocess.PIPE, stderr=subprocess.PIPE, text=True, check=True)
        return result.stdout.strip()
    except subprocess.CalledProcessError as e:
        print(f"Error: {e.stderr}")
        return None
def adb(command):
    return get_command(config.adb_folder + "/adb.exe " + command)
def get_list_adb_devices():
    devices = adb("devices")
    result = []
    for line in devices.splitlines()[1::]:
        line = line.split()
        result.append(line[0])
    return result
