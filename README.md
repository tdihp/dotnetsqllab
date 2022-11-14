# Introduction

This repository is a lab env for reproducing sqlclient connection related
issues.

## How to use

```bash
export CONNSTR='Server=tcp:labsqlpihe.database.windows.net,1433;Initial Catalog=labsql;Persist Security Info=False;User ID=CloudSAa620f28b;Password={};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;ConnectRetryInterval=1;ConnectRetryCount=3;Min Pool Size=2;Max Pool Size=100;'
export BOOTSTRAPQUERYSTR="begin; select * from T1; waitfor delay '00:00:5'; end;"
export QUERYSTR='select * from T1'
export NUMCONNS=10
dotnet run

# for kubernetes
kubectl run --image pihepublic.azurecr.io/dotnetsql:6-4.1.0 \
  --image-pull-policy Always \
  --env='CONNSTR=Server=tcp:{}.database.windows.net,1433;Initial Catalog=labsql;Persist Security Info=False;User ID={};Password={};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;ConnectRetryInterval=1;ConnectRetryCount=3;Min Pool Size=2;Max Pool Size=100;' \
  --env="BOOTSTRAPQUERYSTR=begin; select * from T1; waitfor delay '00:00:5'; end;" \
  --env="QUERYSTR=select * from T1" \
  --env="NUMCONNS=10" \
  dotnetsql
```

