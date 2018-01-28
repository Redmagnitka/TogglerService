clean:
	sbin/clean.sh
restore:
	sbin/restore.sh
build:
	sbin/build.sh
run:
	source .env && sbin/run.sh
test:
	source .env && sbin/test.sh
test-preset:
	source .env && sbin/test-preset.sh

