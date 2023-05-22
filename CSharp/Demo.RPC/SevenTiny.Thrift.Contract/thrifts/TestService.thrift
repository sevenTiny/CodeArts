namespace csharp SevenTiny.Thrift.Contract

service TestService { 
    TestResult Save(1:TestArgs trxn) 
}

enum TestResult { 
    SUCCESS = 0, 
    FAILED = 1, 
}

struct TestArgs { 
    1: required i64 TrxnId; 
    2: required string TrxnName; 
    3: required i32 TrxnAmount; 
    4: required string TrxnType; 
    5: optional string Remark; 
}