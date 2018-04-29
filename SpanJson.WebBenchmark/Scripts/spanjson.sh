echo Get Benchmark
echo Jil
./wrk -H "Accept: application/jil+json"  -t4 -c600 -d120s http://192.168.128.201/api/benchmark
echo SpanJson
./wrk -H "Accept: application/spanjson+json"  -t4 -c600 -d120s http://192.168.128.201/api/benchmark
echo Utf8Json
./wrk -H "Accept: application/utf8json+json"  -t4 -c600 -d120s http://192.168.128.201/api/benchmark
echo Post Benchmark
echo Jil
./wrk -t4 -c600 -d120s -s jil.lua http://192.168.128.201/api/benchmark
echo SpanJson
./wrk -t4 -c600 -d120s -s spanjson.lua http://192.168.128.201/api/benchmark
echo Utf8Json
./wrk -t4 -c600 -d120s -s utf8json.lua http://192.168.128.201/api/benchmark

