import { useEffect, useState } from 'react';
import { api } from '../api/axios';

interface CardReportItem {
    cardNumber: string;
    cardHolder: string;
    balance: number;
}

function CardsPage() {
    const [data, setData] = useState<CardReportItem[]>([]);
    const [loading, setLoading] = useState(true);
    const [cardNumber, setCardNumber] = useState('');
    const [cardHolder, setCardHolder] = useState('');
    const [page, setPage] = useState(1);
    const [totalCount, setTotalCount] = useState(0);
    const pageSize = 10;

    const fetchData = () => {
        setLoading(true);

        const params = {
            cardNumber: cardNumber || undefined,
            cardHolder: cardHolder || undefined,
            page,
            pageSize
        };

        api.get('/Card/cards', { params })
            .then(response => {
                setData(response.data.items);
                setTotalCount(response.data.totalCount);
            })
            .catch(error => {
                console.error('Error fetching cards:', error);
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

    return (
        <div style={{ padding: '2rem' }}>
            <h2>Card Balances Report</h2>

            <div style={{ marginBottom: '1rem' }}>
                <input
                    type="text"
                    placeholder="Card Number"
                    value={cardNumber}
                    onChange={(e) => setCardNumber(e.target.value)}
                    style={{ marginRight: '1rem' }}
                />
                <input
                    type="text"
                    placeholder="Card Holder"
                    value={cardHolder}
                    onChange={(e) => setCardHolder(e.target.value)}
                    style={{ marginRight: '1rem' }}
                />
                <button onClick={handleSearch}>Search</button>
            </div>

            {loading ? <p>Loading...</p> : (
                <>
                    <table border={1} cellPadding={8}>
                        <thead>
                            <tr>
                                <th>Card Number</th>
                                <th>Card Holder</th>
                                <th>Balance</th>
                            </tr>
                        </thead>
                        <tbody>
                            {data.map((card) => (
                                <tr key={card.cardNumber}>
                                    <td>{card.cardNumber}</td>
                                    <td>{card.cardHolder}</td>
                                    <td>{card.balance}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>

                    <div style={{ marginTop: '1rem' }}>
                        <p>Total Results: {totalCount}</p>
                        <button disabled={page <= 1} onClick={() => setPage(page - 1)}>Previous</button>
                        <span style={{ margin: '0 1rem' }}>Page {page}</span>
                        <button disabled={page * pageSize >= totalCount} onClick={() => setPage(page + 1)}>Next</button>
                    </div>
                </>
            )}
        </div>
    );
}

export default CardsPage;
