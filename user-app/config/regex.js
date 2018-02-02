var regex = {
    'number':(obj)=>{
        return /\d{4}$/.test(obj);
    },
    'linecode':(obj)=>{
        return /^\d{8}$/.test(obj)
    },
    'phone':(obj)=>{
        return /^1\d{10}$/.test(obj)
    },
    'isNull':(obj)=>{
        var tag = true;
        Object.keys(obj).map((item,index)=>{
            if (obj[item] === ''){
                tag = false;
            }
        })
        return tag;
    }
}
module.exports = regex;