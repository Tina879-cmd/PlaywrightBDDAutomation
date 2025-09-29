import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  vus: 10,            // virtual users
  duration: '30s',    // test duration
};

const BASE_URL = 'https://parabank.parasoft.com';

export default function () {
  const payload = {
    username: 'TinaPatil',
    password: 'tina12345',
  };

  const headers = {
    'Content-Type': 'application/x-www-form-urlencoded',
  };

  const res = http.post(`${BASE_URL}/parabank/login.htm`, payload, { headers });

  check(res, {
    'status is 200': (r) => r.status === 200,
    'login success detected': (r) => r.body.includes('Welcome'),
  });

  sleep(1);
}
