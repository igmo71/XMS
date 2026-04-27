window.appHttp = {
    post: async (url) => {
        await fetch(url, { method: 'POST' });
    }
};