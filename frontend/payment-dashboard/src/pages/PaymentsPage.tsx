import { useEffect, useState } from 'react';
import { api } from '../api/axios';
import TransactionsStatusChart from '../components/TransactionsStatusChart';

interface PaymentReportItem {
    transactionId: string;
    cardNumber: string;
    amount: number;
    requestedAt: string;
    status: number;
}

function PaymentsPage() {
    const [data, setData] = useState<PaymentReportItem[]>([]);
    const [loading, setLoading] = useState(true);
    const [cardNumber, setCardNumber] = useState('');
    const [status, setStatus] = useState('');
    const [page, setPage] = useState(1);
    const [totalCount, setTotalCount] = useState(0);
    const pageSize = 10;

    const fetchData = () => {
        setLoading(true);

        const params = {
            cardNumber: cardNumber || undefined,
            status: status !== '' ? status : undefined,
            page,
            pageSize
        };

        api.get('/reports/payments', { params })
            .then(response => {
                setData(response.data.items);
                setTotalCount(response.data.totalCount);
            })
            .catch(error => {
                console.error('Error fetching payments:', error);
            })
            .finally(() => setLoading(false));
    };

    useEffect(() => {
        fetchData();
    }, [page]);

    const handleSearch = () => {
        setPage(1);
        fetchData();
    };

    const statusLabel = (s: number) => {
        switch (s) {
            case 0: return 'Held';
            case 1: return 'Confirmed';
            case 2: return 'Refunded';
            default: return 'Unknown';
        }
    };

    return (
        <div style={{ padding: '2rem' }}>
            <h2>Payments Report</h2>

            {/* Search Filters */}
            <div style={{ marginBottom: '1rem' }}>
                <input
                    type="text"
                    placeholder="Card Number"
                    value={cardNumber}
                    onChange={(e) => setCardNumber(e.target.value)}
                    style={{ marginRight: '1rem' }}
                />
                <select value={status} onChange={(e) => setStatus(e.target.value)} style={{ marginRight: '1rem' }}>
                    <option value="">All</option>
                    <option value="0">Held</option>
                    <option value="1">Confirmed</option>
                    <option value="2">Refunded</option>
                </select>
                <button onClick={handleSearch}>Search</button>
            </div>

            {/* Data Table */}
            {loading ? <p>Loading...</p> : (
                <>
                    <table border={1} cellPadding={8} style={{ width: '100%', marginTop: '1rem' }}>
                        <thead>
                            <tr>
                                <th>Transaction ID</th>
                                <th>Card Number</th>
                                <th>Amount</th>
                                <th>Requested At</th>
                                <th>Status</th>
                            </tr>
                        </thead>
                        <tbody>
                            {data.map((item) => (
                                <tr key={item.transactionId}>
                                    <td>{item.transactionId}</td>
                                    <td>{item.cardNumber}</td>
                                    <td>{item.amount}</td>
                                    <td>{new Date(item.requestedAt).toLocaleString()}</td>
                                    <td>{statusLabel(item.status)}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>

                    {/* Pagination */}
                    <div style={{ marginTop: '1rem' }}>
                        <p>Total Results: {totalCount}</p>
                        <button disabled={page <= 1} onClick={() => setPage(page - 1)}>Previous</button>
                        <span style={{ margin: '0 1rem' }}>Page {page}</span>
                        <button disabled={page * pageSize >= totalCount} onClick={() => setPage(page + 1)}>Next</button>
                    </div>
                </>
            )}

            {/* Chart */}
            <TransactionsStatusChart />
        </div>
    );
}

export default PaymentsPage;
