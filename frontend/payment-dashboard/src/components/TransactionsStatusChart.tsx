import { useEffect, useState } from 'react';
import { Bar } from 'react-chartjs-2';
import { Chart, BarElement, CategoryScale, LinearScale } from 'chart.js';
import { api } from '../api/axios';

Chart.register(BarElement, CategoryScale, LinearScale);

interface StatusCounts {
    held: number;
    confirmed: number;
    refunded: number;
}

const TransactionsStatusChart = () => {
    const [counts, setCounts] = useState<StatusCounts>({ held: 0, confirmed: 0, refunded: 0 });

    useEffect(() => {
        // Fetch all data (or filtered) then group by status
        api.get('/reports/payments?page=1&pageSize=1000') // fetch up to 1000 records
            .then(res => {
                const data = res.data.items;
                const statusMap: StatusCounts = { held: 0, confirmed: 0, refunded: 0 };

                data.forEach((t: any) => {
                    if (t.status === 0) statusMap.held++;
                    if (t.status === 1) statusMap.confirmed++;
                    if (t.status === 2) statusMap.refunded++;
                });

                setCounts(statusMap);
            })
            .catch(err => console.error('Chart error', err));
    }, []);

    const chartData = {
        labels: ['Held', 'Confirmed', 'Refunded'],
        datasets: [
            {
                label: 'Transactions',
                data: [counts.held, counts.confirmed, counts.refunded],
                backgroundColor: ['#ffc107', '#28a745', '#dc3545'],
                borderWidth: 1
            }
        ]
    };

    const options = {
        scales: {
            y: { beginAtZero: true }
        }
    };

    return (
        <div style={{ maxWidth: 500, margin: '2rem auto' }}>
            <h3 style={{ textAlign: 'center' }}>Transactions by Status</h3>
            <Bar data={chartData} options={options} />
        </div>
    );
};

export default TransactionsStatusChart;
