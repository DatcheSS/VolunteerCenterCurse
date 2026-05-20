// ── On-page search highlight ───────────────────────────────────────────────
window.searchHighlight = function (query) {
    window.clearSearchHighlights();
    if (!query || query.trim().length < 1) return 0;

    const escaped = query.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
    const main = document.querySelector('.main-content') || document.body;
    let count = 0;

    function walk(node) {
        if (node.nodeType === 3) {
            const text = node.textContent;
            const re = new RegExp(escaped, 'gi');
            if (!re.test(text)) return;

            const frag = document.createDocumentFragment();
            let lastIndex = 0;
            const re2 = new RegExp(escaped, 'gi');
            let m;
            while ((m = re2.exec(text)) !== null) {
                if (m.index > lastIndex)
                    frag.appendChild(document.createTextNode(text.slice(lastIndex, m.index)));
                const mark = document.createElement('mark');
                mark.className = 'vc-highlight';
                mark.textContent = m[0];
                frag.appendChild(mark);
                lastIndex = re2.lastIndex;
                count++;
            }
            if (lastIndex < text.length)
                frag.appendChild(document.createTextNode(text.slice(lastIndex)));
            node.parentNode.replaceChild(frag, node);
        } else if (node.nodeType === 1 &&
            !['SCRIPT', 'STYLE', 'MARK', 'INPUT', 'TEXTAREA', 'SELECT', 'HEADER', 'FOOTER'].includes(node.tagName)) {
            Array.from(node.childNodes).forEach(walk);
        }
    }

    walk(main);

    const first = document.querySelector('mark.vc-highlight');
    if (first) first.scrollIntoView({ behavior: 'smooth', block: 'center' });
    return count;
};

window.clearSearchHighlights = function () {
    document.querySelectorAll('mark.vc-highlight').forEach(mark => {
        mark.parentNode.replaceChild(document.createTextNode(mark.textContent), mark);
    });
};

// ── File download from base64 ──────────────────────────────────────────────
window.downloadFile = function (fileName, base64Data, mimeType) {
    const link = document.createElement('a');
    link.href = 'data:' + mimeType + ';base64,' + base64Data;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};
