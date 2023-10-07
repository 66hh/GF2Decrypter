const protobuf = require("protobufjs")
const util = require('util')
const fs = require('fs')

function decode(file) {

    fs.readFile("Table\\" + file + ".bytes", async (err, data) => {

        var skip = data.readUInt32LE() + 1;

        data = data.subarray(skip)

        const root = await protobuf.load("proto/TextMap.proto")
        const testMessage = root.lookup("TextMapTable")
        const list = testMessage.decode(data)

        fs.writeFile(file + ".json", JSON.stringify(list), function () { })
    })
}

decode("LangPackageTableCnBuiltinData")
decode("LangPackageTableCnData")

console.log("TextMap写出成功")