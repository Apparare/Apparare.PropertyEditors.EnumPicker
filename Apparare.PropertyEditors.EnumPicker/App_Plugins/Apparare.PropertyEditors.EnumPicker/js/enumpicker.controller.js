angular.module("umbraco")
    .controller("enumPickerController", ($scope, validationMessageService, enumPickerResource) => {
        var config = {
            items: []
        }
        Utilities.extend(config, $scope.model.config);
        config.items = enumPickerResource.enumProperties_init($scope.model.config.assembly, $scope.model.config.enum).then((res) => {
            console.log(res);
            config.items = res;
        });
        $scope.model.config = config;

        $scope.$on('formSubmitting', function () {
            if ($scope.model.value && ($scope.model.value.length === 0 || $scope.model.value[0] === null)) {
                $scope.model.value = null;
            }
        });

        convertArrayToDictionaryArray = (model) => {
            var newItems = [];
            for (var i = 0; i < model.length; i++) {
                newItems.push({
                    id: model[i],
                    sortOrder: 0,
                    value: model[i]
                });
            }
            return newItems;
        }

        convertObjectToDictionaryArray = (model) => {
            var newItems = [];
            var vals = _.values($scope.model.config.items);
            var keys = _.keys($scope.model.config.items);
            for (var i = 0; i < vals.length; i++) {
                var label = vals[i].value ? vals[i].value : vals[i];
                newItems.push({
                    id: keys[i],
                    sortOrder: vals[i].sortOrder,
                    value: label
                });
            }
            return newItems;
        }

        $scope.updateDropdownValue = () => {
            $scope.model.value = $scope.model.dropdownValue;
        };

        if (Utilities.isArray($scope.model.config.items)) {
            if (!Utilities.isObject($scope.model.config.items[0])) {
                $scope.model.config.items = convertArrayToDictionaryArray($scope.model.config.items);
            }
        } else if (Utilities.isObject($scope.model.config.items)) {
            $scope.model.config.items = convertObjectToDictionaryArray($scope.model.config.items);
        } else {
            throw 'The items property must be either an array or a dictionary';
        }

        if ($scope.model.value === null || $scope.model.value === undefined) {
            $scope.model.value = '';
        }

        $scope.model.dropdownValue = '';
        if ($scope.model.value) {
            $scope.model.dropdownValue = $scope.model.value;
        }

        validationMessageService.getMandatoryMessage($scope.model.validation).then(function (value) {
            $scope.mandatoryMessage = value;
        });
    });