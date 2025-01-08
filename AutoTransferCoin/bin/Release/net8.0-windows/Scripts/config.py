import json
import os
current_directory = os.path.dirname(os.path.abspath(__file__))
parent_directory = os.path.dirname(current_directory)
with open(parent_directory + "/config.json", 'r') as file:
    data = json.load(file)
adb_folder = data['ADBFolder']
ld_tab = data['LDTab']