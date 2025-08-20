import { useEffect, useState } from "react";
import DeleteConfirmationModal from '../eventCard/DeleteConfirmationModal';
import toastr from "toastr";
import "toastr/build/toastr.min.css";
import '@fortawesome/fontawesome-free/css/all.min.css';


const EventCard = () => {
  // модалка удаления
  const [showModal, setShowModal] = useState(false);

  const handleDelete = async () => {

    // 1. Получаем текущий URL
    const pathD = window.location.pathname;  // Например: "/element/5"
    const partsD = pathD.split("/");          // Разделяем строку по "/"
    const idToDelete = partsD[partsD.length - 1];     // Берем последний элемент (5)

    let pathToDelete = "/api/ref/events/delete/" + idToDelete;
    try {
      const response = await fetch(pathToDelete, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          'RequestVerificationToken': localStorage.getItem('RequestVerificationToken')
        },
        credentials: "include" // обязательно, чтобы отправилась кука XSRF-TOKEN
      });

      if (!response.ok) {
        const errorText = await response.text(); // получаем описание ошибки с сервера
        toastr.error(`Ошибка при удалении: ${errorText}. Попробуйте позже`, "Ошибка");
        return; // прерываем выполнение, чтобы не шло дальше
      }

      if (response.status == 204) {

        // сделать кнопки disabled (не доступными для клика)

        const data = await response.text();

        //  Показать уведомление
        toastr.success("Мероприятие успешно удалено из системы!", "Удалено");

        setTimeout(() => {
          window.location.href = "/events";
        }, 3000);
      }

    } catch (error) {
      // ОБРАБОТКА ОШИБКА *error*
      toastr.error("Произошла системная ошибка. Попробуйте позже.", "Ошибка");
    }

    setShowModal(false);
  };

  // загрузка данных
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const parts = window.location.pathname.split("/");
  const id = parts[parts.length - 1];
  const backUrl = `/api/ref/events/${id}`;

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(backUrl);
        if (!response.ok) {
          const errorText = await response.text();
          alert(`Ошибка: ${errorText}`);
          window.history.go(-1);
          return;
        }
        const result = await response.json();
        setData(result);
      } catch (err) {
        toastr.error("Произошла системная ошибка. Попробуйте позже.", "Ошибка");
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, [id]);

  if (loading) return <p>Загрузка...</p>;
  if (error) return <p>Ошибка: {error}</p>;

  const ev = data.event;

  const totalFinance =
    (Number(ev?.finance?.municipalBudget) || 0) +
    (Number(ev?.finance?.regionalBudget) || 0) +
    (Number(ev?.finance?.granteBudget) || 0) +
    (Number(ev?.finance?.otherBudget) || 0);

  const participantsTotal = Array.isArray(ev?.categories)
    ? ev.categories.reduce((sum, c) => sum + (Number(c.count) || 0), 0)
    : 0;

  const YesNo = (v) => (v ? "Да" : "Нет");

  // Нормализуем ссылки (поддержка массивов строк и объектов)
  const normalizeLinks = (raw) => {
    if (!raw) return [];
    const arr = Array.isArray(raw) ? raw : [raw];

    return arr
      .map((item, i) => {
        const content = typeof item === "string" ? item : item?.content;
        if (!content || typeof content !== "string") return null;

        const trimmed = content.trim();
        const hasProtocol = /^https?:\/\//i.test(trimmed);
        const href = hasProtocol ? trimmed : `https://${trimmed}`;

        return {
          id: (typeof item === "object" && item?.id) || i,
          label: (typeof item === "object" && item?.organizationName) || "Ссылка",
          href,
          text: trimmed,
        };
      })
      .filter(Boolean);
  };

  return (
    <div className="container">
      <div className="event-card">
        {/* заголовок */}
        <h2>Основная информация о мероприятии «{ev?.name ?? ""}»</h2>

        {/* организатор */}
        <section className="sec">
          <label>Организатор</label>
          <div className="kv-badge-line">
            <span className="kv-badge-value">
              {ev?.organizer?.name} ({ev?.organizer?.municipality})
            </span>
          </div>
        </section>

        {/* номер темы */}
        <section className="sec">
          <label>Номер темы</label>
          <div className="kv"><div className="kv-val">{ev?.theme?.code}</div></div>
        </section>

        {/* описание темы */}
        <section className="sec">
          <label>Описание темы</label>
          <div className="kv"><div className="kv-val">{ev?.theme?.description}</div></div>
        </section>

        {/* структурная организация */}
        <section className="sec">
          <label>Структурная организация</label>
          <div className="kv">
            <div className="kv-val">
              {ev?.organizer?.name} ({ev?.organizer?.municipality})
            </div>
          </div>
        </section>

        {/* исполнитель */}
        {ev?.actor && (
          <section className="sec">
            <label>Исполнитель</label>
            <div className="kv"><div className="kv-val">{ev.actor}</div></div>
          </section>
        )}

        {/* наименование */}
        {ev?.name && (
          <section className="sec">
            <label>Наименование мероприятия</label>
            <div className="kv"><div className="kv-val">{ev.name}</div></div>
          </section>
        )}

        {/* дата */}
        {ev?.date && (
          <section className="sec">
            <label>Дата проведения</label>
            <div className="kv"><div className="kv-val">{ev.date}</div></div>
          </section>
        )}

        {/* описание мероприятия */}
        {ev?.content && (
          <section className="sec">
            <label>Краткое описание</label>
            <div className="kv"><div className="kv-val">{ev.content}</div></div>
          </section>
        )}

        {/* Ссылки на материалы */}
        {normalizeLinks(ev?.mediaLinks).length > 0 && (
          <section className="sec">
            <label>Ссылки на материалы</label>
            <div className="badge-list">
              {normalizeLinks(ev?.mediaLinks).map((link) => (
                <div key={link.id} className="badge-item">
                  <span className="chip">{link.label}</span>
                  <div className="desc">
                    <a className="media-link" href={link.href} target="__blank" rel="noopener noreferrer">
                      {link.text}
                    </a>
                  </div>
                </div>
              ))}
            </div>
          </section>
        )}

        {/* уровень/форма */}
        {ev?.levelType && (
          <section className="sec">
            <label>Уровень мероприятия</label>
            <div className="kv"><div className="kv-val">{ev.levelType}</div></div>
          </section>
        )}
        {ev?.eventType && (
          <section className="sec">
            <label>Форма проведения</label>
            <div className="kv"><div className="kv-val">{ev.eventType}</div></div>
          </section>
        )}

        {/* управленческие решения / результат */}
        {ev?.decision && (
          <section className="sec">
            <label>Управленческие решения</label>
            <div className="kv"><div className="kv-val">{ev.decision}</div></div>
          </section>
        )}
        {ev?.result && (
          <section className="sec">
            <label>Результат мероприятия</label>
            <div className="kv"><div className="kv-val">{ev.result}</div></div>
          </section>
        )}

        {/* УЧАСТНИКИ — ПОЛНАЯ ТАБЛИЦА */}
        {ev?.categories && ev.categories.length > 0 && (
          <section className="sec">
            <label>Участники мероприятия</label>

            <div className="table-wrap">
              {/* <table className="participants-table">
                <thead>
                  <tr>
                    <th>Категория</th>
                    <th>Кол-во</th>
                  </tr>
                </thead>
                <tbody>
                  {ev.categories.map((c) => (
                    <tr key={c.id || c.name}>
                      <td className="cat-name">
                        <span className="chip">{c.name}</span>
                      </td>
                      <td className="cat-count">
                        <div className="chip-value">{c.count}</div>
                      </td>
                    </tr>
                  ))}
                </tbody>
                <tfoot>
                  <tr>
                    <td className="sum-label">ВСЕГО</td>
                    <td className="sum-value"><div className="chip-value">{participantsTotal}</div></td>
                  </tr>
                </tfoot>
              </table> */}
              <table className="participants-table">
                <thead>
                  <tr>
                    <th>Категория</th>
                    <th>Количество</th>
                  </tr>
                </thead>
                <tbody>
                  {ev?.categories.map((c) => (
                    <tr key={c.id || c.name}>
                      <td className="cat-name">
                        <span className="chip">{c.name}</span>
                      </td>
                      <td className="cat-count">
                        <div className="chip-value">{c.count}</div>
                      </td>
                    </tr>
                  ))}
                </tbody>
                <tfoot>
                  <tr>
                    <td className="sum-label">ИТОГО</td>
                    <td className="sum-value">
                      <div className="chip-value">{participantsTotal}</div>
                    </td>
                  </tr>
                </tfoot>
              </table>
            </div>
          </section>
        )}

        {/* ФИНАНСИРОВАНИЕ — ПОЛНАЯ ТАБЛИЦА (Описание предпоследней строкой, ИТОГО последней) */}
        {ev?.finance && (
          <section className="sec">
            <label>Финансирование</label>

            <div className="table-wrap">
              <table className="finance-table">
                <thead>
                  <tr>
                    <th>Статья</th>
                    <th>Сумма</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td className="fin-name"><span className="chip">Муниципальный бюджет</span></td>
                    <td className="fin-amount"><div className="chip-value">{ev.finance?.municipalBudget} руб.</div></td>
                  </tr>
                  <tr>
                    <td className="fin-name"><span className="chip">Окружной бюджет</span></td>
                    <td className="fin-amount"><div className="chip-value">{ev.finance?.regionalBudget} руб.</div></td>
                  </tr>
                  <tr>
                    <td className="fin-name"><span className="chip">Гранты/Субсидии</span></td>
                    <td className="fin-amount"><div className="chip-value">{ev.finance?.granteBudget} руб.</div></td>
                  </tr>
                  <tr>
                    <td className="fin-name"><span className="chip">Другое</span></td>
                    <td className="fin-amount"><div className="chip-value">{ev.finance?.otherBudget} руб.</div></td>
                  </tr>
                </tbody>
                <tfoot>
                  <tr>
                    <td className="sum-label">ИТОГО</td>
                    <td className="sum-value">
                      <div className="chip-value">{totalFinance} руб.</div>
                    </td>
                  </tr>
                  <tr>
                    <td className="fin-desc-footer" colSpan="2">
                      Описание:
                      {ev.finance?.description || "—"}
                    </td>
                  </tr>

                </tfoot>
              </table>
            </div>
          </section>
        )}

        {/* ОБРАТНАЯ СВЯЗЬ — только «Описание», последний блок */}
        {ev?.feedBack?.description && (
          <section className="sec">
            <label>Обратная связь</label>

            <div className="table-wrap">
              <table className="finance-table">
                <thead>
                  <tr>
                    <th>Тип</th>
                    <th>Результат</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td className="fin-name"><span className="chip">Интервью </span></td>
                    <td className="fin-amount"><div className="chip-value">{ev.feedBack.hasInterview ? "Да" : "Нет"}</div></td>
                  </tr>
                  <tr>
                    <td className="fin-name"><span className="chip">Анкетирование </span></td>
                    <td className="fin-amount"><div className="chip-value">{ev.feedBack.hasGuestionnaire ? "Да" : "Нет"}</div></td>
                  </tr>
                  <tr>
                    <td className="fin-name"><span className="chip">Онлайн-опрос </span></td>
                    <td className="fin-amount"><div className="chip-value">{ev.feedBack.hasInternet ? "Да" : "Нет"}</div></td>
                  </tr>
                  <tr>
                    <td className="fin-name"><span className="chip">Опрос </span></td>
                    <td className="fin-amount"><div className="chip-value">{ev.feedBack.hasOpros ? "Да" : "Нет"}</div></td>
                  </tr>
                  <tr>
                    <td className="fin-name"><span className="chip">Другое </span></td>
                    <td className="fin-amount"><div className="chip-value">{ev.feedBack.hasOther ? "Да" : "Нет"}</div></td>
                  </tr>
                </tbody>
                <tfoot>
                  <tr>
                    <td className="fin-desc-footer" colSpan="2">
                      Описание:
                      {ev.feedBack.description || "—"}
                    </td>
                  </tr>
                </tfoot>
              </table>
            </div>
            {/* <div className="badge-list">
              <div className="badge-item">
                <span className="chip">Описание</span>
                <div className="chip-value">{ev.feedBack.hasInterview ? "Да" : "Нет"}</div>
                <div className="chip-value">{ev.feedBack.hasGuestionnaire ? "Да" : "Нет"}</div>
                <div className="chip-value">{ev.feedBack.hasInternet ? "Да" : "Нет"}</div>
                <div className="chip-value">{ev.feedBack.hasOpros ? "Да" : "Нет"}</div>
                <div className="chip-value">"Другое": {ev.feedBack.hasOther ? "Да" : "Нет"}</div>
              </div>
            </div> */}
          </section>
        )}


        {/* СОГЛАСОВАНИЯ */}
        {ev?.agreements && ev.agreements.length > 0 && (
          <section className="sec">
            <label>Согласования</label>
            <div className="badge-list">
              {ev.agreements.map((ag) => (
                <div key={ag.id} className="badge-item">
                  <span className="chip">{ag.organization}</span>
                  <div className="chip-value"><span className="muted">Результат: </span>{ag.result}</div>
                  <div className="chip-value"><span className="muted">Описание: </span>{ag.description ?? "Нет"}</div>
                </div>
              ))}
            </div>
          </section>
        )}

        {/* НАРУШЕНИЯ */}
        {ev?.violations && ev.violations.length > 0 && (
          <section className="sec">
            <label>Выявленные нарушения и блокировки</label>
            <div className="badge-list">
              {ev.violations.map((v) => (
                <div key={v.id} className="badge-item">
                  <span className="chip">{v.name}</span>
                  <div className="chip-value">
                    <span className="muted">Заблокировано/Отправлено: </span>{v.blocked}/{v.send}
                  </div>
                  {v.order && (<div className="chip-value"><span className="muted">Приказ: </span>{v.order}</div>)}
                </div>
              ))}
            </div>
          </section>
        )}

        {/* КОНКУРС */}
        {ev?.concourse && (
          <section className="sec">
            <label>Направление на участие в конкурсе</label>
            <div className="badge-list">
              <div className="badge-item">
                <span className="chip">Название/описание</span>
                <div className="chip-value">{ev.concourse.description || "—"}</div>
              </div>
              <div className="badge-item">
                <span className="chip">Результат</span>
                <div className="chip-value">{ev.concourse.result || "—"}</div>
              </div>
              {ev.concourse.details !== undefined && (
                <div className="badge-item">
                  <span className="chip">Описание результата</span>
                  <div className="chip-value">{ev.concourse.details || "—"}</div>
                </div>
              )}
            </div>
          </section>
        )}



        {/* ПОЛУЧАТЕЛЬ ПОДДЕРЖКИ */}
        {ev?.supports && ev?.supports.length > 0 && (
          <section className="sec">
            <label>Получатель поддержки</label>
            <div className="kv"><div className="kv-val">{ev?.supports[0]?.receiver}</div></div>
          </section>
        )}



        {/* ПОДДЕРЖКА */}
        {ev?.supports && ev?.supports.length > 0 && (
          <section>
            <label>Поддержка проекта</label>
            <div className="table-wrap">
              <table className="finance-table">
                <thead>
                  <tr>
                    <th>Тип поддержки</th>
                    <th>Описание поддержки</th>
                  </tr>
                </thead>
                <tbody>
                  {ev?.supports.map((support) => (
                    <tr key={support.id}>
                      <td className="fin-name"><span className="chip">{support.supportType}</span></td>
                      <td className="fin-amount"><div className="chip-value">{support.description}</div></td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </section>
        )}



        {/* ЦЕЛЕВАЯ АУДИТОРИЯ */}
        {ev?.audiences && ev?.audiences.length > 0 && (
          <section>
            <label>Целевая аудитория </label>
            <div className="table-wrap">
              <table className="finance-table">
                <thead>
                  <tr>
                    <th>Категория аудитории</th>
                  </tr>
                </thead>
                <tbody>
                  {ev?.audiences.map((audience) => (
                    <tr key={audience.id}>
                      <td className="fin-name"><span className="chip">{audience.category}</span></td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </section>
        )}



        {/* ДОП. ХАРАКТЕРИСТИКИ — ТАБЛИЦА */}
        {ev?.isValuable != undefined && (
          <section className="sec">
            <label>Дополнительные характеристики</label>

            <div className="table-wrap">
              <table className="extras-table">
                <thead>
                  <tr>
                    <th>Характеристика</th>
                    <th>Значение</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td className="extra-name">
                      <span className="chip">Значимое мероприятие</span>
                    </td>
                    <td className="extra-value">
                      <div className="chip-value">{ev?.isValuable ? "Да" : "Нет" ?? "Нет"}</div>
                    </td>
                  </tr>
                  <tr>
                    <td className="extra-name">
                      <span className="chip">Включено в лучшие практики</span>
                    </td>
                    <td className="extra-value">
                      <div className="chip-value">{ev?.isBestPractice ? "Да" : "Нет" ?? "Нет"}</div>
                    </td>
                  </tr>
                  <tr>
                    <td className="extra-name">
                      <span className="chip">Формат «равный равному»</span>
                    </td>
                    <td className="extra-value">
                      <div className="chip-value">{ev?.equalToEqualDescription == "" ? "Нет" : ev?.equalToEqualDescription}</div>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </section>
        )}


        {/* кнопки */}
        <div className="button-group">
          {data.canDelete && (
            <>
              <button className="delete" type="button" onClick={() => setShowModal(true)}>Удалить</button>
              <DeleteConfirmationModal
                isOpen={showModal}
                onConfirm={handleDelete}
                onCancel={() => setShowModal(false)}
              />
            </>
          )}
          <button className="back" type="button" onClick={() => window.history.back()}>Назад</button>
        </div>
      </div>
    </div>
  );
};

export default EventCard;