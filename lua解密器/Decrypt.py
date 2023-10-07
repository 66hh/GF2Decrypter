from subprocess import run
import os

folder_path = "A5CFF04BAF8EAC27EF4D4716C075F344"
out_folder_path = "LuaScripts"

def xor(data):
    result = bytearray(data)
    for i in range(0, len(data)):
        result[i] ^= 0xff
    return result

def decrypt(file):
    return xor(open(file,"rb").read())

for file_or_folder in os.listdir(folder_path):
    full_path = os.path.join(folder_path, file_or_folder)
    out_full_path = os.path.join(out_folder_path, file_or_folder).replace(".bytes",".luac")
    if os.path.isfile(full_path):
        print(file_or_folder)
        open(out_full_path,"wb").write(decrypt(full_path))
        run("start javaw -jar unluac.jar " + out_full_path + " -o " + out_full_path.replace(".luac",".lua"),shell=True)
