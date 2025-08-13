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

        {/* организатор (без chip-value) */}
        <section className="sec">
          <label>Организатор</label>
          <div className="kv-badge-line">
            <span className="kv-badge-value">
              {ev?.organizer?.name} ({ev?.organizer?.municipality})
            </span>
          </div>
        </section>

        {/* номер темы (без chip-value) */}
        <section className="sec">
          <label>Номер темы</label>
          <div className="kv">
            <div className="kv-val">{ev?.theme?.code}</div>
          </div>
        </section>

        {/* описание темы (без chip-value) */}
        <section className="sec">
          <label>Описание темы</label>
          <div className="kv">
            <div className="kv-val">{ev?.theme?.description}</div>
          </div>
        </section>

        {/* структурная организация (без chip-value) */}
        <section className="sec">
          <label>Структурная организация</label>
          <div className="kv">
            <div className="kv-val">
              {ev?.organizer?.name} ({ev?.organizer?.municipality})
            </div>
          </div>
        </section>

        {/* исполнитель (без chip-value) */}
        {ev?.actor && (
          <section className="sec">
            <label>Исполнитель</label>
            <div className="kv">
              <div className="kv-val">{ev.actor}</div>
            </div>
          </section>
        )}

        {/* наименование (без chip-value) */}
        {ev?.name && (
          <section className="sec">
            <label>Наименование мероприятия</label>
            <div className="kv">
              <div className="kv-val">{ev.name}</div>
            </div>
          </section>
        )}

        {/* дата (без chip-value) */}
        {ev?.date && (
          <section className="sec">
            <label>Дата проведения</label>
            <div className="kv">
              <div className="kv-val">{ev.date}</div>
            </div>
          </section>
        )}


        {/* описание мероприятия (без chip-value) */}
        {ev?.content && (
          <section className="sec">
            <label>Краткое описание</label>
            <div className="kv">
              <div className="kv-val">{ev.content}</div>
            </div>
          </section>
        )}

        {/* уровень/форма: форма без chip-value */}
        {ev?.levelType && (
          <section className="sec">
            <label>Уровень мероприятия</label>
            <div className="kv">
              <div className="kv-val">{ev.levelType}</div>
            </div>
          </section>
        )}
        {ev?.eventType && (
          <section className="sec">
            <label>Форма проведения</label>
            <div className="kv">
              <div className="kv-val">{ev.eventType}</div>
            </div>
          </section>
        )}

        {/* управленческие решения / результат (без chip-value) */}
        {ev?.decision && (
          <section className="sec">
            <label>Управленческие решения</label>
            <div className="kv">
              <div className="kv-val">{ev.decision}</div>
            </div>
          </section>
        )}
        {ev?.result && (
          <section className="sec">
            <label>Результат мероприятия</label>
            <div className="kv">
              <div className="kv-val">{ev.result}</div>
            </div>
          </section>
        )}

        {/* ссылки СМИ (умный рендер) */}
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

        {/* участники мероприятия (значения без chip-value) */}
        {ev?.categories && ev.categories.length > 0 && (
          <section className="sec">
            <label>Участники мероприятия</label>
            <div className="badge-list">
              {ev.categories.map((c) => (
                <div key={c.id} className="badge-item">
                  <span className="chip">{c.name}</span>
                  <div className="chip-value">{c.count}</div>
                </div>
              ))}
            </div>
          </section>
        )}

        {/* финансирование (оставляем chip-value для сумм) */}
        {ev?.finance && (
          <section className="sec">
            <label>Финансирование</label>
            <div className="badge-list">
              <div className="badge-item">
                <span className="chip">Муниципальный бюджет</span>
                <div className="chip-value">{ev.finance?.municipalBudget} руб.</div>
              </div>
              <div className="badge-item">
                <span className="chip">Окружной бюджет</span>
                <div className="chip-value">{ev.finance?.regionalBudget} руб.</div>
              </div>
              <div className="badge-item">
                <span className="chip">Гранты/Субсидии</span>
                <div className="chip-value">{ev.finance?.granteBudget} руб.</div>
              </div>
              <div className="badge-item">
                <span className="chip">Другое</span>
                <div className="chip-value">{ev.finance?.otherBudget} руб.</div>
              </div>
              <div className="badge-item">
                <span className="chip">ИТОГО</span>
                <div className="chip-value">{totalFinance} руб.</div>
              </div>
            </div>
          </section>
        )}

        {/* обратная связь (чипы для значений) */}
        {ev?.feedBack && (
          <section className="sec">
            <label>Обратная связь</label>
            <div className="badge-list">
              <div className="badge-item">
                <span className="chip">Опрос</span>
                <div className="chip-value">{YesNo(ev.feedBack.hasOpros)}</div>
              </div>
              <div className="badge-item">
                <span className="chip">Онлайн-опрос</span>
                <div className="chip-value">{YesNo(ev.feedBack.hasInternet)}</div>
              </div>
              <div className="badge-item">
                <span className="chip">Анкетирование</span>
                <div className="chip-value">{YesNo(ev.feedBack.hasGuestionnaire)}</div>
              </div>
              <div className="badge-item">
                <span className="chip">Интервью</span>
                <div className="chip-value">{YesNo(ev.feedBack.hasInterview)}</div>
              </div>
              <div className="badge-item">
                <span className="chip">Другое</span>
                <div className="chip-value">{YesNo(ev.feedBack.hasOther)}</div>
              </div>
              {ev.feedBack.description && (
                <div className="badge-item">
                  <span className="chip">Описание</span>
                  <div className="chip-value">{ev.feedBack.description}</div>
                </div>
              )}
            </div>
          </section>
        )}

        {/* взаимодействие */}
        {ev?.interAgencyCooperations && ev.interAgencyCooperations.length > 0 && (
          <section className="sec">
            <label>Взаимодействие</label>
            <div className="badge-list">
              {ev.interAgencyCooperations.map((x) => (
                <div key={x.id} className="badge-item">
                  <span className="chip">{x.organization}</span>
                  <div className="chip-value"> Тип взаимодействия: {x.role}</div>
                  {x.description && <div className="chip-value"> Описание: {x.description}</div>}
                </div>
              ))}
            </div>
          </section>
        )}

        {/* поддержка */}
        {ev?.supports && ev.supports.length > 0 && (
          <section className="sec">
            <label>Поддержка</label>
            <div className="badge-list">
              {ev.supports[0]?.receiver && (
                <div className="badge-item">
                  <span className="chip">Получатель поддержки</span>
                  <div className="chip-value"> {ev.supports[0].receiver}</div>
                </div>
              )}
              {ev.supports.map((s) => (
                <div key={s.id} className="badge-item">
                  <span className="chip">Вид поддержки: {s.supportType}</span>
                  <div className="chip-value"> Описание: {s.description}</div>
                </div>
              ))}
            </div>
          </section>
        )}

        {/* аудитории */}
        {ev?.audiences && ev.audiences.length > 0 && (
          <section className="sec">
            <label>Целевая аудитория</label>
            <div className="badge-list">
              {ev.audiences.map((a) => (
                <div key={a.id} className="badge-item">
                  <span className="chip">{a.category}</span>
                </div>
              ))}
            </div>
          </section>
        )}

        {/* отправка материалов */}
        {ev?.theme?.code === "1.3.5" && (
          <section className="sec">
            <label>Отправка материала</label>
            <div className="badge-list">
              <div className="badge-item">
                <span className="chip">Отправка в НАК</span>
                <div className="chip-value">{YesNo(ev.directToNAC)}</div>
              </div>
              <div className="badge-item">
                <span className="chip">Отправка в субъекты</span>
                <div className="chip-value">{ev.directToSubjects ?? "Нет"}</div>
              </div>
            </div>
          </section>
        )}

        {/* согласования */}
        {ev?.agreements && ev.agreements.length > 0 && (
          <section className="sec">
            <label>Согласования</label>
            <div className="badge-list">
              {ev.agreements.map((ag) => (
                <div key={ag.id} className="badge-item">
                  <span className="chip">{ag.organization}</span>
                  <div className="chip-value">
                    <span className="muted">Результат: </span>{ag.result}
                  </div>
                  <div className="chip-value">
                    <span className="muted">Описание: </span>{ag.description ?? "Нет"}
                  </div>
                </div>
              ))}
            </div>
          </section>
        )}

        {/* нарушения */}
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
                  {v.order && (
                    <div className="chip-value">
                      <span className="muted">Приказ: </span>{v.order}
                    </div>
                  )}
                </div>
              ))}
            </div>
          </section>
        )}

        {/* конкурс */}
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

        {/* дополнительные характеристики */}
        <section className="sec">
          <label>Дополнительные характеристики</label>
          <div className="badge-list">
            <div className="badge-item">
              <span className="chip">Значимое мероприятие</span>
              <div className="chip-value">{YesNo(ev?.isValuable)}</div>
            </div>
            <div className="badge-item">
              <span className="chip">Включено в лучшие практики</span>
              <div className="chip-value">{YesNo(ev?.isBestPractice)}</div>
            </div>
            <div className="badge-item">
              <span className="chip">Формат «равный равному»</span>
              <div className="chip-value">{YesNo(ev?.equalToEqualDescription)}</div>
            </div>
          </div>
        </section>

        {/* кнопки */}
        <div className="button-group">
          {data.canDelete && (
            <>
              <button className="delete" type="button" onClick={() => setShowModal(true)}>
                Удалить
              </button>
              <DeleteConfirmationModal
                isOpen={showModal}
                onConfirm={handleDelete}
                onCancel={() => setShowModal(false)}
              />
            </>
          )}
          <button className="back" type="button" onClick={() => window.history.back()}>
            Назад
          </button>
        </div>
      </div>
    </div>
  );
};

export default EventCard;
