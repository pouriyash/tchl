// TODO sync with payesh-helper of ui
﻿function makeKeyTitleObj(arr) {
    return arr.reduce(function (acc, val) {
        var obj = {};
        obj[val.Key] = val.Title;
        return _.assign(acc, obj);
    }, {})
}


function makeSepDict(seps) {
    if (!seps) return [];
    return makeKeyTitleObj(seps);
}


function makeCompDict(seps) {
    if (!seps) return [];
    return seps.reduce(function (acc, sep) {
        return _.assign(acc, makeKeyTitleObj(sep.Components))
    }, {})
}


function farsiIndicators(indicators, sep) {
    // TODO Test it
    var compDict = makeCompDict(sep);
    var sepDict = makeSepDict(sep);
    return indicators.map(function (indicator) {
        return farsiIndicator(indicator, sep);
    })
}


function farsiIndicator(indicator, sep) {
    var compDict = makeCompDict(sep);
    var sepDict = makeSepDict(sep);
    
    return indicator.split(',').map(function(v) {
        var arr = v.split(':');
        return [sepDict[arr[0]], compDict[arr[1]]].join(':');
    }).join(',');
}


function getSepComponentsByKey(seps, sepKey) {
    if (!seps || !seps.length) return;
    var filteredSeps = seps.filter(function (sep) {
        return sep.Key === sepKey;
    });
    if (!filteredSeps || !filteredSeps.length) return;
    return filteredSeps[0].Components
}


function backIndicator(indicators) {
    if (!indicators || _.isEmpty(indicators)) return [];
    return indicators.map(function (indi) {
        return {
            Key: indi.reduce(function (acc, val) {
                return acc ? acc + ',' + val.Key + ':' + val.Component.Key :
                    val.Key + ':' + val.Component.Key;
            }, ''),
            Combination: indi.map(function (val) {
                return {
                    Separation: val.Key,
                    Component: val.Component.Key
                }
            })
        }
    })
}


function frontIndicator(indicators, Separations) {
    if (!indicators || !Separations || _.isEmpty(indicators) ||
        _.isEmpty(Separations)) return [];
    var sepDict = makeSepDict(Separations);
    var compDict = makeCompDict(Separations);

    return indicators.map(function (indi) {
        return indi.Combination.map(function (comb) {
            return {
                Key: comb.Separation,
                Title: sepDict[comb.Separation],
                Component: {
                    Key: comb.Component,
                    Title: compDict[comb.Component]
                }
            }
        })
    })
}


function getSepsBySepKeys(seps, sepKeys) {
    return seps.filter(function (sep) {
        for (i in sepKeys) {
            if (sep.Key === sepKeys[i])
                return true
        }
    })
}


function getSepByCompKey(seps, compKey) {
    return seps.filter(function (sep) {
        for (i in sep.Components) {
            if (sep.Components[i].Key === compKey) {
                return true;
            }
        }
    })
}


function getSepsByCompKeys(seps, compKeys) {
    var result = [];
    compKeys.forEach(function (compKey) {
        var sep = getSepByCompKey(seps, compKey);
        if (sep.length)
            result = _.unionWith(result, sep, SEP_COMPARATOR);
    })
    return result;
}

/**
 * reverse of objectifyString
 * 
 * @param {any} obj 
 * @param {any} sep 
 * @returns 
 */
function stringifyObj(obj, sep) {
    return Object.keys(obj).map(function (v) {
        return v + ':' + obj[v]
    }).join(sep)
}

/**
 * get str like "abc:ret'sep'qwe:tre" return object "{abc: ret, qwe: tre}""get str like "abc:ret'sep'qwe:tre" return object "{abc: ret, qwe: tre}""
 * 
 * @param {any} str 
 * @param {any} sep 
 * @returns 
 */
function objectifyString(str, sep) {
    var obj = {};
    str.split(sep).forEach(function (v) {
        var arr = v.split(':');
        return obj[arr[0]] = arr[1];
    });
    return obj;
}


function SEP_COMPARATOR(a, b) {
    return a.Key === b.Key
}
