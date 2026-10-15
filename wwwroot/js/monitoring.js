let clients = [];

const ONLINE_TIMEOUT = 30 * 1000; // 30 seconds


const esc = v =>
    String(v ?? '').replace(
        /[&<>"']/g,
        c => ({
            '&': '&amp;',
            '<': '&lt;',
            '>': '&gt;',
            '"': '&quot;',
            "'": '&#39;'
        }[c])
    );


const badge = (
    value,
    trueText = 'Connected',
    falseText = 'Disconnected'
) =>
    `<span class="badge ${value ? 'text-bg-success' : 'text-bg-danger'}">
        ${value ? trueText : falseText}
    </span>`;


function updateOnlineStatuses() {
    const now = Date.now();

    clients.forEach(c => {
        if (!c.lastHeartbeat) {
            c.online = false;
            return;
        }

        const heartbeat =
            new Date(c.lastHeartbeat).getTime();

        if (Number.isNaN(heartbeat)) {
            c.online = false;
            return;
        }

        const age =
            now - heartbeat;

        c.online =
            age <= ONLINE_TIMEOUT;
    });

    render();
}


function render() {
    clients.sort((a, b) =>
        a.clientCode.localeCompare(
            b.clientCode
        )
    );

    document.querySelector(
        '#clientsBody'
    ).innerHTML =
        clients.map(c => `
            <tr>
                <td>${esc(c.clientCode)}</td>

                <td>
                    ${badge(
                    c.online,
                    'ONLINE',
                    'OFFLINE'
                )}
                </td>
            </tr>
        `).join('');


    document.querySelector('#total')
        .textContent =
        clients.length;


    document.querySelector('#online')
        .textContent =
        clients.filter(
            x => x.online
        ).length;


    document.querySelector('#offline')
        .textContent =
        clients.filter(
            x => !x.online
        ).length;
}


async function load() {
    try {
        const r =
            await fetch(
                '/api/clients',
                {
                    cache: 'no-store'
                }
            );

        if (!r.ok) {
            console.error(
                'Failed to load clients:',
                r.status
            );

            return;
        }

        clients =
            await r.json();

        updateOnlineStatuses();
    }
    catch (e) {
        console.error(
            'Error loading clients:',
            e
        );
    }
}


load();


// Recalculate ONLINE/OFFLINE
// every 5 seconds
setInterval(
    updateOnlineStatuses,
    5000
);


// Refresh server data
// every 10 seconds
setInterval(
    load,
    10000
);