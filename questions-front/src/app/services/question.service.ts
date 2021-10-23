import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Question } from '../interfaces/question';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  private apiUrl = environment.apiUrl + "/question";
  public questionsListSize = 3;
  public questionCount = 5;

  public quetionsToAnswer: Question[] = [];
  private listOfQuestions = new ReplaySubject<[Question[]]>();

  constructor(
    private httpClient: HttpClient,
  ) {
    this.getQuestionsList().subscribe();
  }

  getQuestions() : Observable<Question[]> {
    return this.httpClient.get<Question[]>(this.apiUrl);
  }

  getList() {
    return this.listOfQuestions.asObservable();
  }

  private getQuestionsList(): Observable<[Question[]]> {
    return this.httpClient.get<[Question[]]>(`${this.apiUrl}/list_size=${this.questionsListSize}&quest_count=${this.questionCount}`).pipe(
      tap(data => {
        this.listOfQuestions.next(data);
      })
    );
  }
}
