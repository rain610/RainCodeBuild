var requestAjax = {
    commonAJax(options){
        return new Promise((resolve,reject)=>{
            wx.request({
                url: options.url,
                data: options.data||'',
                method: options.method||'GET',
                success: resolve,
                fail: reject
            })
        })
    }   
}

module.exports = requestAjax;