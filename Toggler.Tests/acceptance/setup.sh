#!/usr/local/bin/bash


# SETUP
# =====

#echo "${BASH_VERSINFO}"

function dry_run(){
  local res
  [[ ! -z $DRYRUN ]] && res="echo "
  echo -n $res
}

function is_verbose(){
  local res
  [[ ! -z $VERBOSE ]] && res="-v"
  echo -n $res
}

# Uncomment to disable echo.
# Note that if you absolutely need to echo while echo is disable use
#   builtin echo "this will print anyway"
# echo() { :; }

# COMMON VARS
# ===========

token=admin_token
api_base='http://127.0.0.1:5000/api'
resource=toggles

declare -a headers
headers[0]='Content-Type: application/json'
headers[1]="Authorization: Bearer $token"
headers[2]='Content-length: 1'
#echo ${headers[@]}

# USECASES
# ========

# CREATE 3 TOGGLES
# ----------------
for t in a b c; do
  toggle_id=T$t
  cmd=$(cat <<EOC
      $(dry_run) curl $(is_verbose)
      -H'${headers[0]}' -H'${headers[1]}' -H'${headers[2]}'
      -X PUT $api_base/$resource/$toggle_id
EOC
  )
  echo -n $cmd
  eval $cmd
done

# ADD 2 APPS TO SOME TOGGLE'S BL/WL (we do not create apps, we simply add them to toggles' bl/wl")
# ---------------------------------

# Add "app_alpha" to toggle "Tb" blacklist
# ...................................
app_id=app_alpha
app_version=1
toggle_id=Tb
black_or_white=bl
token=admin_token

cmd=$(cat <<EOC
  $(dry_run) curl $(is_verbose)
  -H'${headers[0]}' -H'${headers[1]}'
  -X PUT $api_base/$resource/$toggle_id/$black_or_white
  -d"{\"ID\":\"$app_id\", \"version\": \"$app_version\"}"
EOC
)
echo $cmd
eval $cmd

# Add "app_bravo" to toggle "Tc" whitelist
# ...................................
app_id=app_bravo
app_version=1
toggle_id=Tc
black_or_white=wl
token=admin_token

cmd=$(cat <<EOC
  $(dry_run) curl $(is_verbose)
  -H'${headers[0]}' -H'${headers[1]}'
  -X PUT $api_base/$resource/$toggle_id/$black_or_white
  -d"{\"ID\":\"$app_id\", \"version\": \"$app_version\"}"
EOC
)
echo $cmd
eval $cmd
