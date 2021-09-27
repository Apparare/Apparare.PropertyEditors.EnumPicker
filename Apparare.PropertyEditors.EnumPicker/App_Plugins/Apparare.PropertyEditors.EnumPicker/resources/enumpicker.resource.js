angular.module("umbraco.resources")
    .factory("enumPickerResource", ($http, umbRequestHelper) => {
        return {
            enumProperties_init: (assembly, type) => {
                return umbRequestHelper.resourcePromise(
                    $http.get("/umbraco/backoffice/api/enumapi/getall?a="+assembly+"&t="+type),
                    "Failed to retrieve enum data"
                );
            }
        };
    });