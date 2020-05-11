
export COMMISH_DATABASE="Server=127.0.0.1;Port=3306;Database=commishsql;Uid=$COMMISH_DB_USER;Pwd=$COMMISH_DB_PASS;"
if [ -z "$COMMISH_DB_USER" ]
then
    echo "\$COMMISH_DB_USER is empty"
    # also check if PASS is empty for convenience
    if [ -z "$COMMISH_DB_PASS" ]
    then
        echo "\$COMMISH_DB_PASS is empty"
    fi
# check if DB_PASS is empty when DB_USER is not
elif [ -z "$COMMISH_DB_PASS" ]
then
    echo "$COMMISH_DB_PASS is empty"
else
    # SETUP
    # start minikube. If it is already started, the start routine runs fast as it does not do a full stop and restart
    minikube start
    # port forward for mysql database
    echo "Port-forwarding for commishsql-mysql running in minikube. This process will be halted after the tests finish."
    kubectl port-forward svc/commishsql-mysql 3306 &> portForward.log &
    # save PID of port-forward process to stop it after the script is finished
    PFWD_PID=$!
    echo "port-forward commishsql-mysql PID is $PFWD_PID"

    # EXECUTE API TESTS
    dotnet test Bballsim/ --filter Api

    # TEAR DOWN
    kill $PFWD_PID
fi