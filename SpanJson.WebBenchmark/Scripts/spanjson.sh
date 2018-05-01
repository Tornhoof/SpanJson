echo '# Get Benchmark'
echo '**Jil**'
echo '```'
./wrk -H "Accept: application/jil+json"  -t4 -c600 -d120s http://192.168.128.201/api/benchmark
echo '```'
echo '**SpanJson**'
echo '```'
./wrk -H "Accept: application/spanjson+json"  -t4 -c600 -d60s http://192.168.128.201/api/benchmark
echo '```'
echo '**Utf8Json**'
echo '```'
./wrk -H "Accept: application/utf8json+json"  -t4 -c600 -d120s http://192.168.128.201/api/benchmark
echo '```'
echo '# Post Benchmark'
echo '**Jil**'
echo '```'
./wrk -t4 -c600 -d120s -s jil.lua http://192.168.128.201/api/benchmark
echo '```'
echo **SpanJson**
echo '```'
./wrk -t4 -c600 -d120s -s spanjson.lua http://192.168.128.201/api/benchmark
echo '```'
echo '**Utf8Json**'
echo '```'
./wrk -t4 -c600 -d120s -s utf8json.lua http://192.168.128.201/api/benchmark
echo '```'

