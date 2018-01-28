# Toggler

`Toggler` is a WEB Service that allows a team to elect applications from their
ecosystem to use feature toggles.

An admin can switch toggles ON or OFF, and no matter a toggle is ON or OFF, it
is usable by all applications unless it has a white (WL) or black list (BL),
and depending on the application being on WL or BL it gains access or its
access to the toggle feature gets revoked respectively.

Every time a toggle is updated, a notification is sent to an external "notifier"
system which in turn must be responsible to warn all apps that a toggle has
changed. All apps are in turn responsible to updated themselves.

# Run Toggler

## requirements

Assuming you are on a `*nix` based OS.

Given the fact that we have not yet an external "notifier" system in place we
will fake one by using `requestb.in`.

So visit `requestb.in` to create a `RequestBin` and place it on `.env` file.

    echo export TOGGLER_NOTIFIER=https://requestb.in/:ID > .env

## run it

    dotnet restore
    make run

# Tests

## requirements

- Toggler must be up and running
- [newman](https://github.com/postmanlabs/newman) (CLI test runner)

Please follow along `newman` installation instructions.

### Install `newman` command line test runner

    npm install newman --global

## optionals

- install [postman](http://getpostman.com) (GUI test runner)

## test it

    make test-preset
    make test

# Under the hood

## Evaluate what have been done
Visit that `requestBin` you've used above and inspect it appending `?inspect`
to it, for instance `https://requestb.in/<:id>?inpect` (edit `<:id>` accordingly).

There you will see all the messages that Toggler sent to it.

### test preset

Running `make test-preset` will insert 3 toggles via `Toggler service` itself.
It will also regist 2 applications/services to use those toggles as pictured
bellow.

- Toggle A
- Toggle B
- Toggle C
- App 1 is on TA black list
- App 2 is on TC white list

That being said means that:

- A1 can view TA
- A1 cannot view TB
- A1 cannot view TC
- A2 can view all toggles

[preset ilustration](./Toggler.Tests/test-preset.png)

## Test against `HTTP API` interface

All tests against Toggler service assume that Toggler service is running and
those test presets above, so once again, please run `make run` and
`make test-preset` before `make test`.

To take a `GUI` taste of running tests against `Toggler service` please load
`Toggler.Tests/acceptance/toggler.postman_collection.json` file
into [postman](http://getpostman.com) and also load
`toggler_dev.postman_environment.json` file and configure the runner to use it
before running the tests.

After running `make test` a simple `echo $?` should return `0`, that's a
sign that all tests went good.

# Restrictions and limitations

## By design

- only admin profiles can create and update toggles
- when requesting a toggle a client app must be authorized to use Toggler service
- when requesting a toggle a client app must provide its ID and version
- toggles cannot have a BlackList and WhiteList of Apps at the same time,
  black or white lists are mutually exclusive.

# Not implemented yet

- external "notifier" system
- a toggle repository implementation other than "in memory"



# TODO

- endpoint: GET /toggles/:feature_id/wl
- endpoint: DELETE /toggles/:feature_id/wl
- endpoint: DELETE toggles/:feature_id/wl/:app_id
- endpoint: GET /toggles/:feature_id/bl
- endpoint: DELETE /toggles/:feature_id/bl
- endpoint: DELETE toggles/:feature_id/bl/:app_id
- usecase: retrieve all toggles for a given app (ON, OFF)

# DONE

- usecase: when toggle updates, togglerService alerts its client apps.
- usecase: retrieve specific toggle for app (id, version)
- usecase: create a toggle which black list so that all app can use it except those on BL.
- usecase: create a toggle with a white list that only apps on that WL can use.
- usecase: validate if user is authorized (kind off)
- usecase: create a toggle to be used by all apps.
- usecase: add app on a toggle's black list
- usecase: switch ON/OFF a toggle (must be authorized)
- usecase: only admin can create toggles
