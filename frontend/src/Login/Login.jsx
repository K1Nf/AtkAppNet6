import React, { useState, useEffect } from 'react';
import "../components/EventForm.css";
import toastr from 'toastr';
import 'toastr/build/toastr.min.css';

const Login = () => {
  const [organizationName, setOrganizationName] = useState('');
  const [password, setPassword] = useState('');
  const [showDropdown, setShowDropdown] = useState(false);

  const [municipalsOrganizations, setMunicipalsOrganizations] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const filteredOrgs = municipalsOrganizations.filter((org) =>
    org.toLowerCase().includes(organizationName.toLowerCase())
  );

  const handleOrgChange = (e) => {
    setOrganizationName(e.target.value);
    setShowDropdown(true);
  };

  const handleOrgSelect = (name) => {
    setOrganizationName(name);
    setShowDropdown(false);
  };

  useEffect(() => {
    // Асинхронная функция для запроса
    const fetchData = async () => {
      const urlBack = "/api/ref/auth/organizations";
      try {
        const response = await fetch(urlBack); // Пример URL
        if (!response.ok) {
          const errorText = await response.text(); // получаем описание ошибки с сервера
          toastr.error(`Ошибка при загрузке организаций: ${errorText}`, "Ошибка");
          return; // прерываем выполнение, чтобы не шло дальше
        }
        const result = await response.json();

        setMunicipalsOrganizations(result);
      } catch (error) {
        toastr.error("Произошла системная ошибка. Попробуйте позже.", "Ошибка");
        setError(error.message); // Обрабатываем ошибку, если что-то пошло не так
      } finally {
        setLoading(false); // Завершаем процесс загрузки
      }
    };

    fetchData(); // Выполняем запрос
  }, []); // Пустой массив означает, что эффект сработает только при монтировании компонента


  if (loading) {
    return loading;
  }

  if (error) {
    return error;
  }




  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!organizationName || !password.trim()) {
      toastr.error('Введите организацию и пароль', 'Ошибка');
      return;
    }

    const selectedOrg = municipalsOrganizations.find(org => org === organizationName);

    try {
      const response = await fetch('/api/ref/auth/authorize', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'RequestVerificationToken': localStorage.getItem('RequestVerificationToken')
        },
        body: JSON.stringify({ organizationName, password }),
        credentials: "include"
      });

      if (!response.ok) {
        const errorText = await response.text(); // получаем описание ошибки с сервера

        if(response.status == 500)
        {
          alert(`Ошибка при авторизации: ${errorText}`, "Ошибка");
        }
        else if (errorText.startsWith("Пароль неверен ")) {
          alert(`Ошибка при авторизации: ${errorText}`, "Ошибка");
        }
        else {
          alert("Ошибка при авторизации: Выбрана несуществующая организация", "Ошибка");
        }

        return; // прерываем выполнение, чтобы не шло дальше
      }

      await fetch('/api/ref/auth/csrf-token', {
        method: 'GET',
        credentials: 'include'
      })
        .then(r => r.json())
        .then(data => {
          localStorage.setItem('RequestVerificationToken', data.csrf);
        });

      const message = await response.text();

      toastr.success(message, 'Добро пожаловать! ');

      window.setTimeout(function () {
        window.location.href = '/events';
      }, 2000);

    } catch (err) {

      toastr.error("Произошла системная ошибка. Попробуйте позже.", "Ошибка");
      toastr.error(err.message || 'Ошибка входа', 'Ошибка');
    }
  }

  return (
    <div className="login-page">
      <div className="login-card">
        <div className="login-image">
          <video autoPlay muted loop playsInline>
            <source src="/images/fon.mp4" type="video/mp4" />
            Ваш браузер не поддерживает видео.
          </video>
        </div>
        <form className="login-form" onSubmit={handleSubmit} autoComplete="off">
          <h2>Авторизация</h2>

          <label htmlFor="organization">Организация</label>
          <div style={{ position: 'relative' }}>
            <input
              type="text"
              id="organization"
              value={organizationName}
              onChange={handleOrgChange}
              onFocus={() => setShowDropdown(true)}
              placeholder="Введите или выберите из списка"
            />
            {showDropdown && filteredOrgs.length > 0 && (
              <ul className="autocomplete-list">
                {filteredOrgs.map((org) => (
                  <li
                    key={org}
                    onClick={() => handleOrgSelect(org)}
                    className="autocomplete-item"
                  >
                    {org}
                  </li>
                ))}
              </ul>
            )}
          </div>

          <input
            type="password"
            placeholder="Пароль"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />

          <div className="actions">
            <button type="submit" className="primary-btn">Войти</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Login;
