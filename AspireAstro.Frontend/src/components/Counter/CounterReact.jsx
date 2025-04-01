import { useEffect, useState } from 'react';
import './Counter.css';

const CounterReact = () => {
  const [count, setCount] = useState(0);

  useEffect(() => {
    const timer = setInterval(() => {
      setCount(prev => prev + 1)
    }, 1000)

    return () => {
      timer.clear()
    }
  }, []);

  return (
    <div className="counter" style={{background: '#55bed5'}}>
      {count}
    </div>
  );
}

export default CounterReact;