import sys
import requests
from bs4 import BeautifulSoup
import multiprocessing


url = 'http://nmsgen.com/#'
outfile = 'planet_names.txt'
count = sys.argv[1] if len(sys.argv) > 1 else 1000


def get_name(i):
	# print('getting', i)
	return BeautifulSoup(requests.get(url).content, 'html.parser').find('div', class_='name').find('h2').text

names = multiprocessing.Pool().map(get_name, range(count))

# for name in names:
# 	print(name)

print(count, 'planets written to', outfile)
with open(outfile, 'w') as f:
	f.write('\n'.join(names))